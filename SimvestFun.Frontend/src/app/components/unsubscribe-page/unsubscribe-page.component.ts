import { Component, OnInit } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ActivatedRoute, Router } from "@angular/router";
import { User } from "src/app/models/user";
import { UserService } from "src/app/services/user.service";

@Component({
    selector: 'unsubscribe-page',
    templateUrl: './unsubscribe-page.component.html',
    styleUrls: ['./unsubscribe-page.component.css']
})
export class UnsubscribePageComponent implements OnInit {
    guid: string = "";
    
    constructor(
        private userService: UserService, 
        private router: Router, 
        private route: ActivatedRoute,
        private messageSnack: MatSnackBar
    ) {}

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.guid = params['guid'];
            this.userService.unsubscribeUser(this.guid).subscribe({
                next: (user: User) => { },
                error: err => this.showSnackBar("Something went wrong. Please try again later")
            });
        });
    }
    
    redirectToHome(): void {
        this.router.navigate(['']);
    }
    
    showSnackBar(message: string) {
        this.messageSnack.open(message, "Dismiss", { });
    }
}