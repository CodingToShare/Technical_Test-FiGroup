import http from "../http-common";
import ITaskResponse from "../interfaces/ITaskResponse";
import ITaskPostRequest from "../interfaces/ITaskPostRequest";
import ITaskPutRequest from "../interfaces/ITaskPutRequest";
import ITaskDeleteRequest from "../interfaces/ITaskDeleteRequest";

const config = {
    headers:{
        Authorization: localStorage.getItem("token"),
    }
  };

const get = () => {
    return http.get<Array<ITaskResponse>>("/Task", config);
};

const post = (data: ITaskPostRequest) => {
    return http.post<any>("/Task", data, config).then(result => result.status);
};

const put = (data: ITaskPutRequest) => {
    return http.put<any>("/Task/" + data.id, data, config).then(result => result.status);
};

const deleteTask = (data: ITaskDeleteRequest) => {
    return http.delete<any>("/Task/" + data.id, config).then(result => result.status);
};


const TaskServices = {
    get,
    post,
    put,
    deleteTask,
};

export default TaskServices;