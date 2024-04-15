import { Component, HostListener, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { UserStock } from 'src/app/models/user-stock';
import { AuthService } from 'src/app/services/auth.service';
import { StocksService } from 'src/app/services/stock.service';
import { UserService } from 'src/app/services/user.service';
import { SellDialogComponent } from '../sell-dialog/sell-dialog.component';
import { Title } from '@angular/platform-browser';
import { PortfolioValues } from 'src/app/models/portfolio-values';
import { DatePipe } from '@angular/common';
import { ConfirmResetAccountDialogComponent } from '../confirm-reset-account-dialog/confirm-reset-account-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserActionService } from 'src/app/services/user-action.service';
import { ChangeAvatarDialogComponent } from '../change-avatar-dialog/change-avatar-dialog.component';
import { UpdateUserDetailsDialogComponent } from '../update-user-details-dialog/update-user-details-dialog.component';
import { Follow } from 'src/app/models/follow';

@Component({
  selector: 'app-account-page',
  templateUrl: './account-page.component.html',
  styleUrls: ['./account-page.component.css']
})
export class AccountPageComponent implements OnInit {

  private routeReuseStrategy: any;

  userStocks: UserStock[] = [];
  user?: User;
  connectedUser? : User;
  userId: string = "";
  isTheConnectedUser: boolean = false;
  actionTrigger: Subject<void> = new Subject<void>();
  onSell: Subject<void> = new Subject<void>();
  values: number[] = [];
  dates: string[] = [];
  portfolioValues: PortfolioValues[] = [];
  chartColor: string = '';
  pipe: DatePipe = new DatePipe('en-US');
  chartHeight = 300;
  isLoading: boolean = true;
  avatar: string = "";
  followButtonText: string = "...";
  follow?: Follow;
  followers: User[] = [];

  
  constructor(
    private stocksService: StocksService, 
    private authService: AuthService, 
    private dialog: MatDialog, 
    private route: ActivatedRoute, 
    private userService: UserService,
    private pageTitle: Title,
    private messageSnack: MatSnackBar,
    private actionService: UserActionService,
    private router: Router) {
      this.routeReuseStrategy = this.router.routeReuseStrategy.shouldReuseRoute;
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
     }

  ngOnInit(): void {
    this.userService.getConnectedUser().subscribe(
      connectedUser => {
        if(connectedUser){
          this.connectedUser = connectedUser;
        }
      }
    );
    this.route.params.subscribe(params => {
      this.userId = params['userId'];
      this.showUserPage();
      this.getFollow();
    });
    this.initializeChartData();
    this.onResize();
  }

  showUserPage(): void {
    this.userService.getUser(this.userId).subscribe({
      next: (user: User) => {
        this.user = user;
        this.avatar = this.setGravatar();
        this.pageTitle.setTitle(`${user.name} - Simvest.fun`);
        this.avatar = this.setGravatar();
        if(this.authService.connectedUser?.id == this.userId)
          this.isTheConnectedUser = true;
        this.getUserStocks();
        this.initializeChartData();
        this.getFollowers();
      }
    });
  }
  
  openSellDialog(userStock: UserStock): void {
    const sellDialog = this.dialog.open(SellDialogComponent, {
      data: {userStock: userStock, user: this.user}
    });

    sellDialog.afterClosed().subscribe(res => {
      this.updateUser();
      this.getUserStocks();
      this.actionTrigger.next();
    });
  }

  openResetConfirmDialog(): void {
    const dialogRef = this.dialog.open(ConfirmResetAccountDialogComponent);

    dialogRef.afterClosed().subscribe(result => {

      if(result && this.user){
        this.actionService.getLastAction().subscribe({
          next: result => {
            if(result.actionType.toLocaleLowerCase() !== "reset"){
              if(this.user){
                this.userService.resetUserAccount().subscribe({
                  next: result => {
                    this.user = result;
                    this.authService.setConnectedUser(this.user);
                    this.getUserStocks();
                    this.showSnackBar("Account reset!");
                    this.actionTrigger.next();
                  },
                  error: _ => {
                    this.showSnackBar("Something went wrong. Please try again!");
                  }
                });
              }
            }
            else {
              this.showSnackBar("Account was already reset!");
            }
          }
        });
      }
    });
  }

  openChangePhotoDialog(): void {
    this.dialog.open(ChangeAvatarDialogComponent);
  }

  openChangeDetailsComponent(): void {
    let dialogRef = this.dialog.open(UpdateUserDetailsDialogComponent, {
      data: {user: this.user}
    });

    dialogRef.afterClosed().subscribe(
      resp => {
        if(resp !== "false" && resp !== undefined) {
          if(this.user?.id === this.connectedUser?.id)
            this.updateUser();
        }
      }
    );
  }

  updateUser(): void {
    if(this.user) {
      this.userService.getUser(this.user.id).subscribe({
        next: user => {
          if(user.id === this.authService.connectedUser?.id){
            this.authService.setConnectedUser(user);
          }
          this.user = user;
        }
      });
    }
  }

  getTotalValue(): number {
    let sum = 0;
    this.userStocks.forEach(us => {
      sum += (us.stock.currentPrice * us.unitCount);
    });
    return sum;
  }

  getUserStocks(): void {
    if(this.user)
      this.stocksService.getUserStocksByUserId(this.user.id).subscribe({
        next: userStocks => this.userStocks = userStocks 
      });
  }

  showSnackBar(message: string) {
    this.messageSnack.open(message, "Dismiss", {
    });
  }

  setGravatar(): string {
    return `//www.gravatar.com/avatar/${this.user?.emailHash}?s=120&d=identicon`;
  }

  initializeChartData(): void {
    this.userService.getUserPortfolioValues(this.userId).subscribe({
      next: (user: User) => {
        this.portfolioValues = user.portfolioValues;
        var cnt = 0;
        var firstValue = this.portfolioValues[0].totalPortfolioValue;
        var lastValue = this.portfolioValues[this.portfolioValues.length-1].totalPortfolioValue;
        if(firstValue > lastValue)
          this.chartColor = 'rgba(255, 0, 38, 0.093)';
        else
          this.chartColor = 'rgba(0, 128, 0, 0.093)';
          
        for(let pv of this.portfolioValues){
          this.values.push(pv.totalPortfolioValue);
          var date = this.pipe.transform(pv.timeStamp, 'MMM d');
          if(date && cnt % 2 ==0)
            this.dates.push(date);
          else 
            this.dates.push('');
          cnt++;
        } 
      } 
    });
  }

  getFollow(): void {
    this.userService.getFollow(this.userId).subscribe({
      next: result => {
        this.follow = result;
        this.setFollowButtonText();
      }
    });
  }

  followUser(): void {
    this.userService.followUser(this.userId).subscribe({
      next: resp => {
        this.showSnackBar("Follow successful");
        this.followButtonText = "Unfollow";
        this.getFollowers();
      },
      error: err => this.showSnackBar("Something went wrong, please try again later")
    });
  }

  unfollowUser(): void {
    this.userService.unfollowUser(this.userId).subscribe({
      next: resp => {
        this.showSnackBar("Unfollow successful");
        this.followButtonText = "Follow";
        this.getFollowers();
      },
      error: err => this.showSnackBar("Something went wrong, please try again later")
    });
  }

  setFollowButtonText(): void {
    if(this.follow == undefined){
      this.followButtonText = "Follow";
    }
    else {
      this.followButtonText = "Unfollow";
    }
  }

  followAction(): void {
    if(this.followButtonText == "Follow") {
      this.followUser();
    }
    else {
      this.unfollowUser();
    }
  }

  getFollowers(): void {
    this.userService.getFollowers(this.userId).subscribe({
      next: resp => this.followers = resp,
      error: err => this.showSnackBar("Something went wrong, please try again later")
    });
  }

  ngOnDestroy(): void {
    this.router.routeReuseStrategy.shouldReuseRoute = this.routeReuseStrategy;
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    if(window.innerWidth < 600)
      this.chartHeight = 200;
    else 
      this.chartHeight = 300;
  }
}
