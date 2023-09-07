import React, { useEffect, useState } from 'react';
import { Breadcrumb, Layout, Menu, theme, Col, Row, MenuProps } from 'antd';
import { Outlet, useLocation, useNavigate } from "react-router-dom";
import jwtDecode from 'jwt-decode'


const { Header, Content, Footer } = Layout;

const App: React.FC = () => {
  const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
  const [username, setUsername] = useState('');

  const {
    token: { colorBgContainer },
  } = theme.useToken();

  const navigate = useNavigate();

  const onClick: MenuProps['onClick'] = (e) => {
    switch (e.key) {
      case "1":
        console.log(e);
        return navigate("/Login");
      case "2":
        console.log(e);
        return navigate("/Register");
      case "3":
        console.log(e);
        return navigate("/Task");
      case "4":
        console.log(e);
        return navigate("/");
    }
  };

  function HeaderView() {
    const location = useLocation();
    switch (location.pathname.toLowerCase()) {
      case "/login":
        return "Iniciar Sesion"
      case "/register":
        return "Registrarse"
      case "/task":
        return "Tareas"
      case "/":
        return "Inicio"
    }
    return <span>Path : {location.pathname}</span>
  }

  useEffect(() => {
    const jwtToken = localStorage.getItem("token");
    console.log(jwtToken);

    if (jwtToken == null) {
      setIsAuthenticated(true);
      setUsername('');
      
    } else {
      setIsAuthenticated(false);     
      const decodedToken: any = jwtDecode(jwtToken);
      let name = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
      setUsername(name);
    }
    console.log(isAuthenticated);
  }, []);

  return (
    <Layout className="layout">
      <Header style={{ alignItems: 'center' }}>
        <div className="demo-logo" />
        <Menu
          onClick={onClick}
          theme="dark"
          mode="horizontal"
        >
          <Menu.Item key="3">Inicio</Menu.Item>
          {isAuthenticated && (
            <Menu.Item key="1">Iniciar Sesion</Menu.Item>
          )}
          {isAuthenticated && (
            <Menu.Item key="2">Registrarse</Menu.Item>
          )}
          {!isAuthenticated && (
            <Menu.Item key="3">Tareas</Menu.Item>
          )}
          {!isAuthenticated && (
            <div key="5" style={{ color: 'white', marginLeft: 'auto' }}>Hola, {username}</div>
          )}
        </Menu>
      </Header>
      <Content style={{ padding: '0 50px' }}>
        <Breadcrumb style={{ margin: '16px 0' }}>
          <Breadcrumb.Item>{HeaderView()}</Breadcrumb.Item>
        </Breadcrumb>
        <div className="site-layout-content" style={{ background: colorBgContainer, padding: 50 }}>
          <Row gutter={[16, 16]}>
            <Col span={12} offset={6}>
              <Outlet />
            </Col>
          </Row>
        </div>
      </Content>
      <Footer style={{ textAlign: 'center' }}>Ant Design ©2023 Created by Ant UED</Footer>
    </Layout>
  );
};

export default App;
