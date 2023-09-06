import React from 'react';
import { Button, Checkbox, Form, Input, notification } from 'antd';
import { useNavigate } from "react-router-dom";
import AuthServices from "./services/AuthServices";
import ILoginRequest from "./interfaces/ILoginRequest";

type FieldType = {
    username?: string;
    password?: string;
    remember?: string;
};

const Login: React.FC = () => {

    console.log("llamado Login");

    const navigate = useNavigate();

    const onFinish = (values: any) => {
        console.log('Success:', values);
        let data: ILoginRequest = {
            email: values.username,
            password: values.password
        }
        AuthServices.login(data)
                    .then((response: any) => {
                        console.log(response);
                        return navigate("/Task");
                    }).catch((e: Error) => {
                        openNotificationWithIcon();
                        console.log(e);
                    });
    };

    const onFinishFailed = (errorInfo: any) => {
        console.log('Failed:', errorInfo);
        openNotificationWithIcon();
    };

    const [api, contextHolder] = notification.useNotification();

    const openNotificationWithIcon = () => {
        api['error']({
            message: 'Error Inicio Sesion',
            description:
                'Error al Iniciar sesion el usuario.',
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

                <Form.Item<FieldType>
                    name="remember"
                    valuePropName="checked"
                    wrapperCol={{ offset: 8, span: 16 }}
                >
                    <Checkbox>Remember me</Checkbox>
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
export default Login;