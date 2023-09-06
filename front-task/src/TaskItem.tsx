import React, { useState } from 'react';
import { List, Input, Space, Button, Select } from 'antd';
import ITaskResponse from "./interfaces/ITaskResponse";
import ITaskStatusResponse from "./interfaces/ITaskStatusResponse";

const TaskItem: React.FC<{ item: ITaskResponse, status: ITaskStatusResponse[], index: number, deleteFuncion: Function, editFuncion: Function }> = ({ item, status, index, deleteFuncion, editFuncion }) => {

    const [isEdit, setIsEdit] = useState<boolean>(false);

    const { Option } = Select;

    const [selectedValue, setSelectedValue] = useState("");

    function EditComponent(bool: boolean, descriptionTask: string) {
        if (bool) {
            return <p><strong># {index + 1}</strong> <Space direction="vertical" size="middle" style={{ width: '100%' }}>
                <Space.Compact style={{ width: '100%' }}>
                    <Input id={item.taskID} addonAfter={Item(status, item.taskStatusID)} placeholder={item.taskDescription}/>
                    <Button type="primary" onClick={() => Edit(item.taskID)}>Editar</Button>
                </Space.Compact>
            </Space> </p>
        }

        return <p><strong># {index + 1}</strong> {item.taskDescription} - {DefineStatus(item.taskStatusID)}</p>


    }

    function Edit(id: string) {

        let contenido = document.getElementById(id) as HTMLInputElement;
        let taskStatus = status.find((x) => { return x.taskStatusName == selectedValue });
        let taskStatusID = taskStatus?.taskStatusID;

        editFuncion(id, contenido.value, taskStatusID)

        setIsEdit(false);

    }

    function handleSelectChange(value: string) {
        console.log(value);
        setSelectedValue(value);
    }

    function Item(array: ITaskStatusResponse[], id: string) {
        let taskStatus = status.find((x) => { return x.taskStatusID == id });
        if (array[0] != undefined) {
            return (<Select defaultValue={taskStatus?.taskStatusName} onChange={handleSelectChange}>
                {array.map((x) => { return <Option value={x.taskStatusName} id={x.taskStatusID}>{x.taskStatusName}</Option> })}
            </Select>)
        }
    }

    function SetEditComponent() {
        if (isEdit) {
            setIsEdit(false);
        } else {
            setIsEdit(true);
        }
    }

    function DefineStatus(id: string) {
        let taskStatus = status.find((x) => { return x.taskStatusID == id });
        return taskStatus?.taskStatusName
    }


    return (

        <>
            <List.Item actions={[<a key="list-loadmore-edit" onClick={SetEditComponent} >edit</a>, <a key="list-loadmore-more" onClick={() => deleteFuncion(item.taskID)}>delete</a>]}>
                <List.Item.Meta
                    description={EditComponent(isEdit, item.taskDescription)}
                />
            </List.Item>
        </>
    );

}

export default TaskItem;