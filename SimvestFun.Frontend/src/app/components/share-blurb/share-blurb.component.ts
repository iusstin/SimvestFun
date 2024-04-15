import { Component, OnInit } from '@angular/core';
import { NgNavigatorShareService } from 'ng-navigator-share';

@Component({
  selector: 'app-share-blurb',
  templateUrl: './share-blurb.component.html',
  styleUrls: ['./share-blurb.component.css']
})
export class ShareBlurbComponent implements OnInit {

  private ngNavigatorShareService: NgNavigatorShareService;

  private userAgent = navigator.userAgent;

  isMobile?: boolean;

  constructor(ngNavigatorShareService: NgNavigatorShareService) {
    this.ngNavigatorShareService = ngNavigatorShareService;
  }

  ngOnInit(): void {
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini|Mobile|mobile|CriOS/i.test(this.userAgent))
      this.isMobile = true;
  }

  async shareApi() {
    try {
      const sharedResponse = await this.ngNavigatorShareService.share({
        title: 'Simvest Fun',
        text: 'I really like this simple investing simulator: ',
        url: window.document.location.href
      });
    } catch (error) {
      console.log('You app is not shared, reason: ', error);
    }
  }
}
