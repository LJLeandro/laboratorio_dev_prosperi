import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit{
  ordens: OrdemDeServico[] = [];
  response: ResponseMessage = null;
  url: string = ""

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + "api/ordemdeservico";

    http.get<ResponseMessage>(this.url)
      .subscribe(
        data => {
          this.response = data

          if (this.response.sucesso == true) {
            this.ordens = this.response.retorno
          }
        },
        error => console.log(error)
      );
  }

  ngOnInit(): void {
    
  }

  
}

interface ResponseMessage {
  sucesso: boolean;
  mensagem: string;
  retorno: any;
}

