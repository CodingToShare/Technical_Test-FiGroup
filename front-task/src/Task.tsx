import React, { useState, useEffect } from 'react';
import { Button, Input, Space, Col, Row, Select, List, notification } from 'antd';
import TaskServices from "./services/TaskServices";
import TaskStatusServices from "./services/TaskStatusServices";
import ITaskResponse from "./interfaces/ITaskResponse";
import ITaskStatusResponse from "./interfaces/ITaskStatusResponse";
import ITaskPostRequest from "./interfaces/ITaskPostRequest";
import ITaskDeleteRequest from "./interfaces/ITaskDeleteRequest";
import ITaskPutRequest from "./interfaces/ITaskPutRequest";
import TaskItem from "./TaskItem"

const { Search } = Input;

type PaginationPosition = 'bottom';

type PaginationAlign = 'center';

type NotificationType = 'success' | 'info' | 'warning' | 'error';

const Task: React.FC = () => {

  const [api, contextHolder] = notification.useNotification();

  const [value, setValue] = useState<ITaskResponse[]>();

  const [valueStatus, setValueStatus] = useState<ITaskStatusResponse[]>([]);

  const [filter, setFilter] = useState<ITaskResponse[]>();

  const { Option } = Select;

  const [position] = useState<PaginationPosition>('bottom');
  const [align] = useState<PaginationAlign>('center');

  const [selectedValue, setSelectedValue] = useState("");

  const openNotificationWithIcon = (type: NotificationType, title: string, content: string) => {
    api[type]({
      message: title,
      description:
        content,
    });
  };

  const onSearch = (valueFilter: string) => {
    setFilter(value?.filter((x)=>x.taskDescription.includes(valueFilter)))
  };

  useEffect(() => {
    UpdateTable();

    handleSelectChange("No completada");

    TaskStatusServices.get()
      .then((response: any) => {
        console.log(response.data);
        setValueStatus(response.data)
      }).catch((e: Error) => {
        console.log(e);
      });
  }, []);

  function Create() {
    let contenido = document.getElementById('TxtTask') as HTMLInputElement;
    let taskStatus = valueStatus.find((x) => { return x.taskStatusName == selectedValue });
    let taskStatusID = taskStatus?.taskStatusID;

    if (taskStatusID != undefined) {
      let interfaceTask: ITaskPostRequest = {
        taskStatusID: taskStatusID,
        taskDescription: contenido.value
      };
      TaskServices.post(interfaceTask).then(() => {
        UpdateTable();
        openNotificationWithIcon('success', 'Exito al crear Tarea', 'La tarea se pudo crear satisfactoriamente');
      }).catch((e: Error) => {
        console.log(e);
        openNotificationWithIcon('error', 'Error al crear Tarea', 'No se pudo crear la tarea');
      });
    } else {
      openNotificationWithIcon('error', 'Error al crear Tarea', 'No se pudo crear la tarea');
    }
  }

  function Item(array: ITaskStatusResponse[]) {
    if (array[0] != undefined) {
      return (<Select defaultValue={array[0].taskStatusName}  onChange={handleSelectChange}>
        {array.map((x) => { return <Option value={x.taskStatusName} id={x.taskStatusID}>{x.taskStatusName}</Option> })}
      </Select>)
    }
  }

  function handleSelectChange(value: string) {
    console.log(value);
    setSelectedValue(value);
  }

  function UpdateTable(){
    TaskServices.get()
      .then((response: any) => {
        console.log(response.data);
        setValue(response.data)
        setFilter(response.data)
      }).catch((e: Error) => {
        console.log(e);
      });
  }


  function DeleteTask(IDTask:string){
    let interfaceTask: ITaskDeleteRequest = {
      id: IDTask,
    };
    TaskServices.deleteTask(interfaceTask).then(() => {
      UpdateTable();
      openNotificationWithIcon('success', 'Exito al eliminar Tarea', 'La tarea se pudo eliminar satisfactoriamente');
    }).catch((e: Error) => {
      console.log(e);
      openNotificationWithIcon('error', 'Error al eliminar Tarea', 'No se pudo eliminar la tarea');
    });
  }

  function EditTask(IDTask:string, taskDescrip:string, IDtaskStatus:string){
    let interfaceTask: ITaskPutRequest = {
      id: IDTask,
      taskDescription: taskDescrip,
      taskStatusID: IDtaskStatus,
    };
    TaskServices.put(interfaceTask).then(() => {
      UpdateTable();
      openNotificationWithIcon('success', 'Exito al editar Tarea', 'La tarea se pudo editar satisfactoriamente');
    }).catch((e: Error) => {
      console.log(e);
      openNotificationWithIcon('error', 'Error al editar Tarea', 'No se pudo editar la tarea');
    });
  }

  return (
    <>
      {contextHolder}
      <div>
        <Space direction="vertical" size="middle" style={{ width: '100%' }}>
          <Space.Compact style={{ width: '100%' }}>
            <Input placeholder='Escriba su Tarea' id='TxtTask' addonAfter={Item(valueStatus)} />
            <Button type="primary" onClick={Create}>Crear</Button>
          </Space.Compact>
        </Space>
        <Row>
          <Col span={12} offset={12} style={{ paddingTop: 20 }}>
            <Search placeholder="input search text" onSearch={onSearch} enterButton style={{ width: '100%' }} />
          </Col>
        </Row>

        <>
          <List
            pagination={{ position, align }}
            dataSource={filter}
            renderItem={(item, index) => (
              <TaskItem index={index} item={item} status={valueStatus} deleteFuncion={DeleteTask} editFuncion={EditTask}></TaskItem>
            )}
          />
        </>
      </div>
    </>
  );
};

export default Task;