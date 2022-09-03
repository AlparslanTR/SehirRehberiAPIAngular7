import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(private AuthService:AuthService) { }

  loginUser:any={}
  ngOnInit() {
  }
  login(){
    this.AuthService.login(this.loginUser);
  }
  logOut(){
    this.AuthService.logOut();
  }
  get isAuthenticated(){
    return this.AuthService.loggedIn();
  }
}
