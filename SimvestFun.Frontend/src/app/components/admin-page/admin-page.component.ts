import { Component, OnInit, ViewChild} from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { SettingsService } from 'src/app/services/settings.service';
import  {CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Setting } from 'src/app/models/setting';

@Component({
  selector: 'app-admin-page',
  templateUrl: './admin-page.component.html',
  styleUrls: ['./admin-page.component.css']
})
export class AdminPage implements OnInit {

  announcementMessage = "";
  @ViewChild('autosize') autosize: CdkTextareaAutosize | undefined;

  constructor(private settingService: SettingsService,
              private messageSnack: MatSnackBar) {}

  ngOnInit(): void {
    this.settingService.getSettingByKey("Announcement").subscribe(
      announcement => this.announcementMessage = announcement ? announcement.value : ""
    )
  }

  saveAnnouncement(): void {
    let announcement : Setting = {
      key: "Announcement",
      value: this.announcementMessage
    }

    this.settingService.updateAnnouncement(announcement).subscribe({
      next: _ => this.showSnackBar("Announcement posted!"),
      error: _ => this.showSnackBar("Something went wrong!")
    });
  }

  deleteAnnouncement(): void {
    let announcement : Setting = {
      key: "Announcement",
      value: ""
    }

    this.settingService.updateAnnouncement(announcement).subscribe({
      next: _ => {
        this.showSnackBar("Announcement deleted!");
        this.announcementMessage = "";
      },
      error: _ => this.showSnackBar("Something went wrong!")
    });
  }

  showSnackBar(message: string) {
    this.messageSnack.open(message, "Dismiss", {
    });
  }
}
