import { Register } from './../models/register';
import { LoginUser } from './../models/loginUser';
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { JwtHelper, tokenNotExpired } from 'angular2-jwt';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private httpclient:HttpClient,private router:Router) { }
  path="https://localhost:44334/api/Auth/";
  userToken:any;
  decodedToken:any;
  jwtHelper:JwtHelper= new JwtHelper();


  login(LoginUser:LoginUser){

    let headers = new HttpHeaders();
    headers=headers.append("content-Type","application/json");
    this.httpclient.post(this.path+"login",LoginUser,{headers:headers,responseType:'text'}).subscribe(data=>{
      this.saveToken(data)
      this.userToken=data
      this.decodedToken=this.jwtHelper.decodeToken(data.toString())
      alert("Giriş Başarılı");
      this.router.navigateByUrl('/city')
    });
  }
  saveToken(token){
    localStorage.setItem('token',token);
  }

  register(register:Register){
    let headers = new HttpHeaders();
    headers=headers.append("content-Type","application/json");
    this.httpclient.post(this.path+"register",register,{headers:headers}).subscribe(data=>{

    });
  }
  logOut(){
    localStorage.removeItem("token")
  }
  loggedIn(){
    return tokenNotExpired("token")
  }
  get token(){
    return localStorage.getItem("token");
  }
  getCurrentUserId(){
    return this.jwtHelper.decodeToken(localStorage.getItem("token")).nameId
  }

}
