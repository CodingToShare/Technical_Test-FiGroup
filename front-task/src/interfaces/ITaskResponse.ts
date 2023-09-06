export default interface ITaskResponse {
    taskID: string,
    taskDescription: string,
    taskStatusID: string,
    createdBy: string,
    created: Date,
    modifiedBy: string,
    modified: Date,
  }