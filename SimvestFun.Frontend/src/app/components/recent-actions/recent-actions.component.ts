import { DatePipe } from '@angular/common';
import { Component, HostListener, OnInit } from '@angular/core';
import { User } from 'src/app/models/user';
import { UserAction } from 'src/app/models/user-action';
import { UserActionService } from 'src/app/services/user-action.service';

@Component({
  selector: 'app-recent-actions',
  templateUrl: './recent-actions.component.html',
  styleUrls: ['./recent-actions.component.css']
})
export class RecentActionsComponent implements OnInit {

  actions: UserAction[] = [];
  pipe: DatePipe = new DatePipe('en-US');
  colNr: number = 2;

  constructor(private userActionService: UserActionService) { }

  ngOnInit(): void {
    this.onResize();
    this.userActionService.getAllActions().subscribe(
      actions => {
        this.actions = actions;
      }
    );
  }

  convertToLocal(date: Date): Date {
    var stringDate = this.pipe.transform(date, "MMM d, y, HH:mm") + ' UTC';
    var newDate = new Date(stringDate);
    return newDate;
  }

  getGravatar(user: User): string {
    return `//www.gravatar.com/avatar/${user.emailHash}?s=16&d=identicon`;
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    if(window.innerWidth < 1140)
      this.colNr = 1;
    else
      this.colNr = 2;
  }

}
