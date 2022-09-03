import { AuthService } from './../../services/auth.service';
import { City } from './../../models/city';
import { CityService } from './../../services/city.service';
import { Component, OnInit } from '@angular/core';
import {FormGroup,FormControl,Validators,FormBuilder} from "@angular/forms";


@Component({
  selector: 'app-city-add',
  templateUrl: './city-add.component.html',
  styleUrls: ['./city-add.component.css'],
  providers:[CityService]
})
export class CityAddComponent implements OnInit {

  constructor(private cityService:CityService,private formBuilder:FormBuilder,private AuthService:AuthService) { }

  city:City;
  cityAddForm:FormGroup;

  createCityForm(){
    this.cityAddForm=this.formBuilder.group(
    {name:["",Validators.required],
    description:["",Validators.required]

    })
  }
  ngOnInit() {
    this.createCityForm();
  }
  addCity(){
    if(this.cityAddForm.valid){
      this.city=Object.assign({},this.cityAddForm.value)
      this.city.userId=this.AuthService.getCurrentUserId();
      this.cityService.addCity(this.city);

    }
  }
  get isAuthenticated(){
    return this.AuthService.loggedIn();
  }
  }

