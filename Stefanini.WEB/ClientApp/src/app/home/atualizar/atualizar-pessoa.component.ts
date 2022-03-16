import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppService } from '../../service';

@Component({
  selector: 'app-atualizar-pessoa',
  templateUrl: './atualizar-pessoa.component.html'
})
export class AtualizarPessoaComponent implements OnInit {

  constructor(private service: AppService, private activatedRouter: ActivatedRoute) { }

  error: any[] = [];
  cidades: any[] = [];
  estados: any[] = [];
  estado = null;
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
      .subscribe(
        (res: any) => { this.pessoa = res.dados; },

        (erro) => {
          this.error = [{ erro: erro.error }];
        }
      );

    this.service.listarcidades()
      .subscribe(
        (res: any) => {
          this.estados = res.dados;
          this.cidades = res.dados;
        },

        (erro) => {
          this.error = [{ erro: erro.error }];
        }
      );
  }

  atualizarpessoa() {
    if (this.pessoa.nome.length > 300) {
      this.error = [{ erro: "Nome muito longo!" }];
      return;
    }
    if (this.pessoa.nome.length < 5) {
      this.error = [{ erro: "Nome muito curto." }];
      return;
    }
    if (this.pessoa.cpf.length != 11) {
      this.error = [{ erro: "CPF inválido!" }];
      return;
    }
    if (this.pessoa.idade < 1) {
      this.error = [{ erro: "Informe um idade válida!" }];
      return;
    }
    if (this.pessoa.cidade.uf == "") {
      this.error = [{ erro: "Informe o Estado" }];
      return;
    }
    if (this.pessoa.cidade.nome == "") {
      this.error = [{ erro: "Informe a Cidade" }];
      return;
    }

    this.error = [];

    this.service.atualizarpessoa(this.meuid, this.pessoa)
      .subscribe(
        () => { },

        (erro) => {
          this.error = [{ erro: erro.error }];
        },

        () => { location.reload(); }
      );
  }

  exibeCidades() {
    this.service.carregarcidades(this.pessoa.cidade.uf)
      .subscribe(
        (res: any) => { this.cidades = res; },

        (erro) => {
          this.error = [{ erro: erro.error }];
        }
      );

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

