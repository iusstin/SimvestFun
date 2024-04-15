import { Component } from '@angular/core';

@Component({
  selector: 'app-change-avatar-dialog',
  templateUrl: './change-avatar-dialog.component.html',
  styleUrls: ['./change-avatar-dialog.component.css']
})
export class ChangeAvatarDialogComponent {

  redirectToGravatar(): void {
    window.open("https://en.gravatar.com/", "_blank");
  }

}
