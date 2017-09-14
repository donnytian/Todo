import { Routes } from '@angular/router';

import { TodoComponent } from './todo.component';

export const todoRoutes: Routes = [
    {
        path: '', pathMatch: 'full', component: TodoComponent
    }
];
