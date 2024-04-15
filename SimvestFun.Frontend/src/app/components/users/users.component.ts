import { Component, HostListener, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { PageEvent } from '@angular/material/paginator';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { UsersPaging } from 'src/app/models/users-paging';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-all-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class AllUsersComponent implements OnInit {

  pagesCount: number = 0;
  pageSize: number = 20;
  users: User[] = [];
  name: FormControl = new FormControl("");
  pageIndex: number = 0;
  window: Window = {} as Window;
  totalLength: number = 0;

  constructor(
    private userService: UserService, 
    private router: Router,
    private pageTitle: Title) { }

  ngOnInit(): void {
    this.pageTitle.setTitle("Users - Simvest.fun");
    this.getUsersByPageIndex(1, this.name.value);
    this.window = window;
  }

  changePage(event: PageEvent): void {
    this.pageIndex = event.pageIndex;
    this.getUsersByPageIndex(this.pageIndex+1, this.name.value);
  }

  getUsersByPageIndex(pageIndex: number, name: string): void {
    this.userService.getUsersByPageIndex(pageIndex, name).subscribe({
      next: (usersPaging: UsersPaging) => {
        this.users = usersPaging.users;
        this.pagesCount = usersPaging.totalPages;
        this.totalLength = usersPaging.totalSize;
      }
    });
  }

  searchUser() {
      this.getUsersByPageIndex(1, this.name.value);
  }

  goToUserPage(userId: string): void {
    this.router.navigate([`user/${userId}`]);
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.window = this.window;
  }
}
