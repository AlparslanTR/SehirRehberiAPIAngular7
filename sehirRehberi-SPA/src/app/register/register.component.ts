import { FormBuilder,FormGroup,FormControl,Validators } from '@angular/forms';
import { AuthService } from './../services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private AuthService:AuthService,private FormBuilder:FormBuilder) { }
  registerForm:FormGroup;
  registerUser:any={};
  ngOnInit() {
    this.createRegisterForm();
  }

  createRegisterForm(){
    this.registerForm=this.FormBuilder.group({
      userName:["",Validators.required],
      password:["",[Validators.required,Validators.minLength(6),Validators.maxLength(20)]],
      confirmPassword:["",Validators.required]
    },
    {validator:this.passwordMatchValidator}
    )
  }
  passwordMatchValidator(g:FormGroup){
    return g.get('password').value===g.get('confirmPassword').value?null:{missmatch:true}
  }
  register(){
    if(this.registerForm.valid){
      this.registerUser=Object.assign({},this.registerForm.value)
      this.AuthService.register(this.registerUser)
      alert("Kayıt Başarılı Hoşgeldiniz "+this.registerUser.userName);
    }
  }
  get isAuthenticated(){
    return this.AuthService.loggedIn();
  }
}
