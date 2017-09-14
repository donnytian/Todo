import { Component, OnInit, AfterViewInit } from '@angular/core';

import { TodoService } from '../../../services/todo.service';

declare var $: any;

export class TodoModel {
    id: string;
    content: string;
    dueDate?: Date;
    completed: boolean;
    message: string;
}

@Component({
    selector: 'todo-cmp',
    templateUrl: './todo.component.html'
})
export class TodoComponent implements OnInit, AfterViewInit {
    constructor(private todo: TodoService) {
        this.model = new TodoModel();
    }

    public todoItems: TodoModel[];
    public model: TodoModel;

    public ngOnInit() {
        this.getItems();
    }

    ngAfterViewInit() {
        // Seems there is no hook to trigger when all child views initialized, so we do materila.init in each page.
        $.material.init();
    }

    private onSubmit(): void {
        this.todo.addItem(this.model).subscribe(
            data => {
                this.model.id = data.id;
                this.todoItems = [this.model].concat(this.todoItems);
                this.model = new TodoModel();
            });
    }

    private getItems(): void {
        this.todo.getItems().subscribe(
            data => {
                this.todoItems = data;
            });
    }
}
