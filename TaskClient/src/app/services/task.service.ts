import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { ADD_TASK, TASK } from '../models/task.models';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private connection!: HubConnection;

  constructor() {
    this.connection = new HubConnectionBuilder()
      .withUrl('/tasks')
      .build();

    this.connection.start().catch(err => console.error(err));
  }

  addTask(task: ADD_TASK) {
    this.connection.invoke('AddTask', task);
  }

  onTaskUpdateReceived(callback: (task: TASK) => void) {
    this.connection.on('TaskAdded', (task: TASK) => {
      callback(task);
    });
  }
}
