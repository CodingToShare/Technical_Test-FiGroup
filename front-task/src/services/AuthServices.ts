import http from "../http-common";
import IRegisterRequest from "../interfaces/IRegisterRequest";
import ILoginRequest from "../interfaces/ILoginRequest";

const register = (data: IRegisterRequest) => {
    return http.post<IRegisterRequest>("/Auth/Register", data).then(result => result.status);
};

const login = (data: ILoginRequest) => {
    return http.post<string>("/Auth/login", data).then(result => {
        console.log(result.data);
        localStorage.setItem("token", "bearer " + result.data);
        return true;
    });
};


const AuthServices = {
    register,
    login,
};

export default AuthServices;