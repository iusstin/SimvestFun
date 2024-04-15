import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-leaderboard',
  templateUrl: './leaderboard.component.html',
  styleUrls: ['./leaderboard.component.css']
})
export class LeaderboardComponent implements OnInit, OnDestroy {

  private eventsSubscription: Subscription = new Subscription;
  dataSource = new MatTableDataSource<User>([]);
  parseFloat = parseFloat;
  parseInt = parseInt;
  leaderboardUsers: User[] = [];
  topUsers: User[] = [];
  connectedUser?: User;
  @Input() events: Observable<void> = new Observable<void>();

  columns = [
    {
      columnDef: 'index',
      header: 'Leaderboard',
      cell: () => ``,
      cell2: (user: User) => `${user.currentPosition === null || user.yesterdayPosition === null ? 0 : user.currentPosition - user.yesterdayPosition}`
    },
    {
      columnDef: 'avatar',
      cell: (user: User) => `${this.setGravatar(user)}`,
      cell2: () => ``
    },
    {
      columnDef: 'name',
      cell: (user: User) => `${user.name}`,
      cell2: () => ``
    },
    {
      columnDef: 'portfolio',
      cell: (user: User) => `${user.totalPortfolioValue}`,
      cell2: (user: User) => `${user.portfolioChange}`
    },
  ];

  displayedColumns = this.columns.map(c => c.columnDef);

  constructor(private userService: UserService,
              private router: Router,
              private authService: AuthService
              ) { }

  ngOnInit(): void {
    this.getTopUsers();
    this.eventsSubscription = this.events.subscribe(() => this.getTopUsers());
  }

  getTopUsers(): void {
    this.userService.getUserLeaderboard().subscribe({
      next: data => {
        this.leaderboardUsers = data;
        this.dataSource = new MatTableDataSource(this.leaderboardUsers);
        this.setTopUserChanges();
        this.connectedUser = this.authService.getConnectedUser(); 
        if(this.connectedUser && this.leaderboardUsers.includes(this.connectedUser)){
          this.leaderboardUsers[this.leaderboardUsers.indexOf(this.connectedUser)] = this.connectedUser;
        }
      }
    });
  }

  setTopUserChanges(): void {
    this.topUsers = this.leaderboardUsers.concat();
    this.topUsers.sort(function(u1, u2){return (Math.abs(u2.portfolioChange) - Math.abs(u1.portfolioChange))});
    this.topUsers = this.topUsers.slice(0,3);
  }

  setGravatar(user: User): string {
    return `//www.gravatar.com/avatar/${user.emailHash}?s=24&d=identicon`;
  }

  redirectToAllUsersPage(): void {
    this.router.navigate(['/users']);
  }

  goToUserPage(userId: string): void {
    this.router.navigate([`user/${userId}`]);
  }

  ngOnDestroy(): void {
    this.eventsSubscription?.unsubscribe();
  }
}
