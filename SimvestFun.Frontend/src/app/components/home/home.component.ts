import { Component, OnInit } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { User } from "src/app/models/user";
import { UserAction } from "src/app/models/user-action";
import { AuthService } from "src/app/services/auth.service";
import { UserActionService } from "src/app/services/user-action.service";
import { MatSnackBar } from '@angular/material/snack-bar';
import { DatePipe } from '@angular/common';
import { UserService } from "src/app/services/user.service";
import { Subject } from "rxjs";
import { Setting } from "src/app/models/setting";
import { SettingsService } from "src/app/services/settings.service";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  hide: boolean = false;
  newUser?: User;
  bonusAction?: UserAction;
  dailyBonus: boolean = false;
  updateBonus: Subject<void> = new Subject<void>();

  announcementMessage = ""

  constructor(
    private pageTitle:Title, 
    private authService: AuthService, 
    private actionService: UserActionService,
    private userService: UserService,
    private messageSnack: MatSnackBar,
    private settingsService : SettingsService) { }

  ngOnInit() {
    this.pageTitle.setTitle("Simvest.fun - Investing simulator for top US companies");
    this.hide = localStorage.getItem('hide description') == 'true';
    this.checkDailyBonus();
    this.settingsService.getSettingByKey("Announcement").subscribe(
      announcement => this.announcementMessage = announcement ? announcement.value : ""
    )
  }

  hideDescription(): void {
    this.hide = true;
    localStorage.setItem('hide description', 'true');
  }

  checkDailyBonus(): void {
    this.actionService.checkLastBonusAction().subscribe({
      next: result => {
        if(result == false && this.authService.getConnectedUser()){
          this.userService.addDailyBonus().subscribe({
            next: user => {
              this.newUser = user;
              this.showSnackBar("Welcome back! You received the $10 daily bonus.");
              this.authService.setConnectedUser(this.newUser);
              this.updateBonus.next();
            },
            error: err => {
              this.showSnackBar("Something went wrong. Please try again!");
            }
          });
        }
      }
    });
  }

  showSnackBar(message: string) {
    this.messageSnack.open(message, "Dismiss", {
      duration: 5000
    });
  }
}
