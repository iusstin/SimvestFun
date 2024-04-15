import { Directive, ElementRef, OnInit, Renderer2 } from "@angular/core";
import { User } from "../models/user";
import { AuthService } from "../services/auth.service";

@Directive({
    selector:'[appGravatar]'
})
export class GravatarDirective implements OnInit {
    user?: User;

    constructor(private el: ElementRef, private renderer: Renderer2, private authService: AuthService){}

    ngOnInit(): void {
        this.user = this.authService.getConnectedUser();
        if(this.user)
            this.setGravatar(this.user);
    }

    setGravatar(user: User): void {
        const GravatarUrl= `//www.gravatar.com/avatar/${user.emailHash}?s=24&d=identicon`;
        this.renderer.setAttribute(this.el.nativeElement, 'src', GravatarUrl);
    }
}