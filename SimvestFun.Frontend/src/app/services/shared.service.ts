import { Injectable } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable({
    providedIn: 'root'
})
export class SharedService {

    constructor(private messageSnack: MatSnackBar) { }

    validPassword(newPassword: string, repeatPassword: string, formGroup: FormGroup): boolean {
        if (newPassword.length < 6) {
            this.showSnackBar("Password must have at least 6 characters!");
            return false;
        }
        if (!new RegExp("[a-z]").test(newPassword)) {
            this.showSnackBar("Password must contain at least one lowercase letter!");
            return false;
        }
        if (!new RegExp("[0-9]").test(newPassword)) {
            this.showSnackBar("Password must contain at least one digit number!");
            return false;
        }
        if (!new RegExp("[^a-zA-Z0-9]+").test(newPassword)) {
            this.showSnackBar("Password must contain at least one non-alphanumeric character!(ex: /, *, _)");
            return false;
        }
        if (newPassword.toLowerCase() === newPassword) {
            this.showSnackBar("Password must contain at least one uppercase letter!");
            return false;
        }
        if (formGroup.invalid) {
            this.showSnackBar("Invalid data!")
            return false;
        }
        if (newPassword !== repeatPassword) {
            this.showSnackBar("Passwords don't match!");
            return false;
        }
        return true;
    }

    private showSnackBar(message: string): void {
        this.messageSnack.open(message, "Dismiss", {
        });
    }
}