import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogInputData } from 'src/app/models/dialog-input-data';
import { AuthService } from 'src/app/services/auth.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-update-user-details-dialog',
  templateUrl: './update-user-details-dialog.component.html',
  styleUrls: ['./update-user-details-dialog.component.css']
})

export class UpdateUserDetailsDialogComponent implements OnInit {

  newName = "";
  newAbout = "";

  constructor(@Inject(MAT_DIALOG_DATA) public data: DialogInputData,
              public dialogRef: MatDialogRef<UpdateUserDetailsDialogComponent>,
              private userService: UserService,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.newName = this.data.user.name;
    this.newAbout = this.data.user.about;
  }

  updateUserInfo(): void {
    let newUser = this.data.user;
    newUser.name = this.newName;  
    newUser.about = this.newAbout;
    newUser.token = "";
    
    if(this.data.user.id === this.authService.connectedUser?.id){
      this.userService.updateUserDetails(newUser).subscribe (
        res => {
          this.dialogRef.close(res);
        }
      );
    }
    else {
      this.userService.updateUserDetailsByAdmin(newUser).subscribe(
        res => {
          this.dialogRef.close(res);
        }
      )
    }
  }
}
