import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { User } from 'src/app/models/user';
import { UserAction } from 'src/app/models/user-action';
import { UserActionService } from 'src/app/services/user-action.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-user-action-history',
  templateUrl: './user-action-history.component.html',
  styleUrls: ['./user-action-history.component.css']
})
export class UserActionHistoryComponent implements OnInit, OnDestroy {

  private eventsSubscription: Subscription = new Subscription;
  readonly arrowRight = "keyboard_arrow_right"

  @Input('user')
  user: User | undefined
  userActions: UserAction[] | undefined;
  dateOffset: string | undefined;
  @Input() events: Observable<void> = new Observable<void>();
  pipe: DatePipe = new DatePipe('en-US');
  currentYear = new Date().getFullYear();

  loadedActionsCount = 0;
  readonly loadingStep = 20;
  canLoadMore = true;

  constructor(private userActionService: UserActionService) { }

  ngOnInit(): void {
    this.loadActions(this.loadingStep);
    this.eventsSubscription = this.events?.subscribe(
      () => {
        if(this.loadedActionsCount < this.loadingStep){
          this.loadActions(this.loadedActionsCount + 1);
        }
        else{
          this.loadActions(this.loadedActionsCount);
          this.canLoadMore = true;
        } 
      }
    );
  }

  loadActions(numberOfActions: number): void {
    if (this.user) {
      this.userActionService.getActionsByUser(this.user.id, numberOfActions).subscribe(
        data => {
          this.userActions = data.userActions;
          if(data.userActions.length === data.allActionsCount){
            this.canLoadMore = false;
          }
          this.loadedActionsCount = data.userActions.length;
        }
      )
    }
  }

  getYear(date: Date): number {
    return this.convertToLocal(date).getFullYear();
  }

  ngOnDestroy(): void {
    this.eventsSubscription?.unsubscribe();
  }

  convertToLocal(date: Date): Date {
    var utcDate = this.pipe.transform(date, "MMM d, y, HH:mm") + ' UTC';
    return new Date(utcDate);
  }
}
