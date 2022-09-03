import { CityService } from './../services/city.service';
import { City } from './../models/city';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-city',
  templateUrl: './city.component.html',
  styleUrls: ['./city.component.css'],
  providers:[CityService]
})
export class CityComponent implements OnInit {

  constructor(private cityService:CityService) { }
  cities:City[];
  ngOnInit() {
    this.cityService.getCities().subscribe(data=>{
      this.cities=data;
    });
  }

}
