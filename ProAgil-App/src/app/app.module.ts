
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { DateTimeFormatPipePipe } from './_helps/DateTimeFormatPipe.pipe';

import { BrowserModule } from '@angular/platform-browser';

import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';

import { EventoService } from './_services/evento.service';

import { AppComponent } from './app.component';
import { EventoComponent } from './evento/evento.component';
import { NavComponent } from './nav/nav.component';

@NgModule({
  declarations: [
    AppComponent,
    EventoComponent,
    NavComponent,
    DateTimeFormatPipePipe
  ],
  imports: [
    BrowserModule,
    BsDropdownModule.forRoot(),
    BrowserAnimationsModule,
    BsDatepickerModule.forRoot(),
    TooltipModule.forRoot(),
    ModalModule.forRoot(),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    EventoService,
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
