import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppService } from '../../service';

@Component({
  selector: 'app-atualizar-pessoa',
  templateUrl: './atualizar-pessoa.component.html'
})
export class AtualizarPessoaComponent implements OnInit {

  constructor(private service: AppService, private activatedRouter: ActivatedRoute) { }

  pessoa: Pessoa = {
    nome: "",
    idade: 0,
    cpf: "",
    cidade: {
      nome: "",
      uf: ""
    }
  };

  meuid = Number(this.activatedRouter.snapshot.paramMap.get('id'));

  ngOnInit(){
    const id = Number(this.activatedRouter.snapshot.paramMap.get('id'));

    this.service.buscarpessoa(id)
      .subscribe((res: any) => {
        this.pessoa = res.dados;
      });
  }

  atualizarpessoa() {
    console.log(this.pessoa);
    this.service.atualizarpessoa(this.meuid, this.pessoa).subscribe((res) => {
      location.reload();
      
    });
  }
}

interface Pessoa {
  nome: string;
  idade: number;
  cpf: string;
  cidade: Cidade;
}

interface Cidade {
  nome: string;
  uf: string;
}

