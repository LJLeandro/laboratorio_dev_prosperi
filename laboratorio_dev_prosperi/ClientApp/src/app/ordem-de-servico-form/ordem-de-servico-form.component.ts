import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-ordem-de-servico-form',
  templateUrl: './ordem-de-servico-form.component.html',
  styleUrls: ['./ordem-de-servico-form.component.css']
})
export class OrdemDeServicoFormComponent implements OnInit {

  private url: string = "";

  ordemDeServicoForm = new FormGroup({
    numero_servico: new FormControl('numero-ordem-de-servico'),
    titulo_servico: new FormControl('titulo-servico'),
    cnpj: new FormControl('cnpj'),
    valor: new FormControl('valor'),
    nome_cliente: new FormControl('nome-do-cliente'),
    cpf_prestador: new FormControl('cpf-prestador'),
    nome_prestador: new FormControl('nome-prestador'),
  })

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.url = baseUrl + 'api\ordemdeservico';
  }

  ngOnInit() {

  }

  onSubmit(): void {
    var formData: any = new FormData();
    formData.append("numero_servico", this.ordemDeServicoForm.get('numero-ordem-de-servico')?.value);
    formData.append("titulo_servico", this.ordemDeServicoForm.get('titulo-servico')?.value);

    console.log("Titulo do Serviço: " + this.ordemDeServicoForm.get('titulo-servico')?.value);
  }
}
