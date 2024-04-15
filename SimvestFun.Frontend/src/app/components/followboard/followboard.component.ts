import { Component, Input, OnInit } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { Router } from "@angular/router";
import { Observable, Subscription } from "rxjs";
import { User } from "src/app/models/user";
import { AuthService } from "src/app/services/auth.service";
import { UserService } from "src/app/services/user.service";

@Component({
    selector: 'app-followboard',
    templateUrl: './followboard.component.html',
    styleUrls: ['./followboard.component.css']
})
export class FollowBoardComponent implements OnInit {

    private eventsSubscription: Subscription = new Subscription;
    dataSource = new MatTableDataSource<User>([]);
    followboardUsers: User[] = [];
    parseFloat = parseFloat;
    @Input() events: Observable<void> = new Observable<void>();
    
    columns = [
        {
            columnDef: 'index',
            header: 'Followboard',
            cell: () => ``,
            cell2: () => ``
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
        public authService: AuthService) {}

    ngOnInit(): void {
        this.getFollowedUsers();
        this.eventsSubscription = this.events.subscribe(() => this.getFollowedUsers());
   }

    getFollowedUsers(): void {
        this.userService.getFollowedUsers().subscribe({
            next: result => {
                this.followboardUsers = result;
                this.dataSource = new MatTableDataSource(result);
            }
        });
    }

    setGravatar(user: User): string {
      return `//www.gravatar.com/avatar/${user.emailHash}?s=24&d=identicon`;
    }

    goToUserPage(userId: string): void {
      this.router.navigate([`user/${userId}`]);
    }
}