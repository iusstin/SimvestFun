import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-application-header',
  templateUrl: './application-header.component.html',
  styleUrls: ['./application-header.component.css']
})
export class ApplicationHeaderComponent implements OnInit {
  isAccPage: boolean = false;

  constructor(public authService: AuthService, public router: Router, private userService: UserService) { }

  ngOnInit(): void {
    if(localStorage.getItem("token")) {
      this.userService.getConnectedUser().subscribe({
        next: (user: User | null) => {
          if(user)
            this.authService.setConnectedUser(user);
        }
      });
    }
  }

  redirectToLogin(): void {
    this.router.navigate(['/login']);
  }

  redirectToRegister(): void {
    this.router.navigate(['/register']);
  }

  redirectToAccount(): void {
    this.router.navigate([`/user/${this.authService.connectedUser?.id}`]);
  }

  redirectToHome(): void {
    this.router.navigate(['']);
  }

  logOutUser(): void {
    this.authService.logOutUser();
  }

}
