import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { AtualizarPessoaComponent } from './home/atualizar/atualizar-pessoa.component';
import { CidadeComponent } from './cidade/cidade.component';
import { AtualizarCidadeComponent } from './cidade/atualizar/atualizar-cidade.component';

import { AppService } from './service';

import { DataTablesModule } from "angular-datatables";
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    AtualizarPessoaComponent,
    CidadeComponent,
    AtualizarCidadeComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    DataTablesModule,
    SweetAlert2Module.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'pessoa/atualizar/:id', component: AtualizarPessoaComponent },
      { path: 'cidade', component: CidadeComponent },
      { path: 'cidade/atualizar/:id', component: AtualizarCidadeComponent },
    ])
  ],
  providers: [AppService],
  bootstrap: [AppComponent]
})
export class AppModule { }
