import { Component, OnInit  } from "@angular/core";
import { Router } from "@angular/router";
import { User } from "src/app/models/user";
import { UserService } from "src/app/services/user.service";

@Component({
    selector: 'app-application-footer',
    templateUrl: './application-footer.component.html',
    styleUrls: ['./application-footer.component.css']
})
export class ApplicationFooterComponent implements OnInit {

    connectedUser?: User;

    constructor(private userService: UserService, public router: Router) {}

    ngOnInit(): void {
        this.userService.getConnectedUser().subscribe(
            user => {
                if(user){
                    this.connectedUser = user;
                }
            }
        );
    }
}