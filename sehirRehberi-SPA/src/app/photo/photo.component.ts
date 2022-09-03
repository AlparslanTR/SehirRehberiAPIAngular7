import { Photo } from './../models/photo';
import { Component, OnInit } from '@angular/core';
import { FileUploader, FileUploadModule } from 'ng2-file-upload';
import { AuthService } from './../services/auth.service';
import { ActivatedRoute } from '@angular/router';


@Component({
  selector: 'app-photo',
  templateUrl: './photo.component.html',
  styleUrls: ['./photo.component.css']
})
export class PhotoComponent implements OnInit {

  constructor(private AuthService:AuthService,private ActivatedRoute:ActivatedRoute) { }

  Photo:Photo[]=[];
  uploader:FileUploader;
  hasBaseDropZoneOver=false;
  path="https://localhost:44334/api/";
  currentMain:Photo;
  currentCity:any;

  ngOnInit() {
    this.ActivatedRoute.params.subscribe(params=>{
      this.currentCity=params["cityId"]
    })
    this.initializeUploader();
  }
  initializeUploader(){
    this.uploader=new FileUploader({
      url:this.path+'cities/'+this.currentCity+'/photos',
      authToken:'Bearer '+localStorage.getItem('token'),
      isHTML5:true,
      allowedFileType:['image'],
      autoUpload:false,
      removeAfterUpload:true,
      maxFileSize:10*1024*1024
    });

    this.uploader.onSuccessItem=(item,response,status,headers)=>{
      if(response)
      {
        const res:Photo=JSON.parse(response);
        const photo={
          id:res.id,
          url:res.url,
          dateAdded:res.dateAdded,
          description:res.description,
          isMain:res.isMain,
          cityId:res.cityId
        }
        this.Photo.push(photo)
      }
    }

  }
}
