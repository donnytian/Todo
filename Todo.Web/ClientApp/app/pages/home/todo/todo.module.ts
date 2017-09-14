import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { TodoComponent } from './todo.component';
import { todoRoutes } from './todo.routing';
import { TodoService } from '../../../services/todo.service';

@NgModule({
    imports: [
        CommonModule,
        RouterModule.forChild(todoRoutes),
        FormsModule,
    ],
    declarations: [TodoComponent],
    providers: [
        TodoService
    ]
})

export class TodoModule {}
