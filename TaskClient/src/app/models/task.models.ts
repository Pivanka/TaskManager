export interface ADD_TASK {
  title: string,
  description: string,
  userId: number,
  deadline: Date
}

export interface TASK {
  id: number,
  title: string,
  description: string,
  isCompleted: boolean,
  deadline: Date
}
