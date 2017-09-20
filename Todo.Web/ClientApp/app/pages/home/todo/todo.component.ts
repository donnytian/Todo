import { Component, OnInit, AfterViewInit } from '@angular/core';

import { TodoService } from '../../../services/todo.service';

declare var $: any;
declare var swal: any;

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
        this.newItem = new TodoModel();
    }

    public todoItems: TodoModel[];
    public newItem: TodoModel;
    public editingItem: TodoModel;
    public editingContent: string;

    public ngOnInit() {
        this.getItems();
    }

    ngAfterViewInit() {
        // Seems there is no hook to trigger when all child views initialized, so we do materila.init in each page.
        $.material.init();
        //  Activate the tooltips
        $('[rel="tooltip"]').tooltip();
    }

    private trackById(index: number, item: TodoModel): string {
        return item.id;
    }

    private onSubmit(): void {
        this.todo.addOrUpdate(this.newItem).subscribe(
            data => {
                this.newItem.id = data.id;
                this.todoItems = [this.newItem].concat(this.todoItems);
                this.newItem = new TodoModel();
            });
    }

    private getItems(): void {
        this.todo.getItems().subscribe(
            data => {
                this.todoItems = data;
            });
    }

    private onToggle(item: TodoModel): void {
        this.todo.addOrUpdate(item).subscribe(
            data => {
                console.log(data);
            });
    }

    private onEdit(item: TodoModel): void {
        this.editingItem = item;
        this.editingContent = item.content;
    }

    private onSave(): void {
        if (!this.editingContent) {
            return;
        }

        this.editingItem.content = this.editingContent;
        this.todo.addOrUpdate(this.editingItem).subscribe(
            data => {
                this.editingItem = null;
                this.editingContent = null;
            });
    }

    private onCancel(): void {
        this.editingItem = null;
        this.editingContent = null;
    }

    private onRemove(item: TodoModel): void {
        swal({
            title: 'Are you sure to delete?',
            text: 'You will not be able to revert this!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonClass: 'btn btn-success',
            cancelButtonClass: 'btn btn-danger',
            confirmButtonText: 'Yes, delete it!',
            buttonsStyling: false
        }).then(_ => {
            this.todo.delete(item.id).subscribe(
                data => {
                    console.log("item deleted.");
                    this.todoItems = this.todoItems.filter(el => el !== item);
                });
        },
            dismiss => {
                // dismiss can be 'overlay', 'cancel', 'close', 'esc', 'timer'
                if (dismiss === 'cancel') {
                }
            });
    }
}
