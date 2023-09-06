import http from "../http-common";
import ITaskStatusResponse from "../interfaces/ITaskStatusResponse";

const config = {
    headers:{
      Authorization: localStorage.getItem("token"),
    }
  };

const get = () => {
    return http.get<Array<ITaskStatusResponse>>("/TaskStatus", config);
};

const TaskStatusServices = {
    get,
};

export default TaskStatusServices;