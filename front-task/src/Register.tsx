import React from 'react';
import { Button, Checkbox, Form, Input, notification } from 'antd';
import { useNavigate } from "react-router-dom";
import AuthServices from "./services/AuthServices";
import IRegisterRequest from "./interfaces/IRegisterRequest";

let registerSuccess = false;



type FieldType = {
    username?: string;
    password?: string;
};

const Register: React.FC = () => {

    console.log("llamado Register");

    const navigate = useNavigate();

    const onFinish = (values: any) => {
        let data: IRegisterRequest = {
            email: values.username,
            password: values.password
        }
        console.log(values);
        AuthServices.register(data)
            .then((response: any) => {
                AuthServices.login(data)
                    .then((response: any) => {
                        registerSuccess = true
                        console.log(response);
                        return navigate("/Task");
                    }).catch((e: Error) => {
                        openNotificationWithIcon();
                        console.log(e);
                    });
            })
            .catch((e: Error) => {
                openNotificationWithIcon();
                console.log(e);
            });
    };

    const onFinishFailed = (errorInfo: any) => {
        console.log('Failed:', errorInfo);
        openNotificationWithIcon()
    };

    const [api, contextHolder] = notification.useNotification();

    const openNotificationWithIcon = () => {
        api['error']({
            message: 'Error Registro',
            description:
                'Error al registrar el usuario.',
        });
    };
    return (
        <>
            {contextHolder}
            <Form
                name="basic"
                labelCol={{ span: 8 }}
                wrapperCol={{ span: 16 }}
                style={{ maxWidth: 600 }}
                initialValues={{ remember: true }}
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
                autoComplete="off"
            >
                <Form.Item<FieldType>
                    label="Username"
                    name="username"
                    rules={[{ required: true, message: 'Please input your username!' }]}
                >
                    <Input />
                </Form.Item>

                <Form.Item<FieldType>
                    label="Password"
                    name="password"
                    rules={[{ required: true, message: 'Please input your password!' }]}
                >
                    <Input.Password />
                </Form.Item>

                <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
                    <Button type="primary" htmlType="submit">
                        Submit
                    </Button>
                </Form.Item>
            </Form>
        </>
    );
};
export default Register;