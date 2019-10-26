import axios from "axios";
import history from "../Helpers/History";
import {LOGIN_USER, AUTH_ERROR, GET_MESSAGE, GET_HOME_PAGE_MESSAGE, REGISTER_USER} from "../Types/types";

const url = "https://localhost:5001/api";

export function login({Email, Password}) {
    return function (dispatch) {
        axios.post(`${url}/user/auth/login`, {Email, Password})
            .then(response => {

                const loggedUser = response.data;

                dispatch({type: LOGIN_USER, token: loggedUser.token});

                if (localStorage !== null) {
                    localStorage.clear()
                }

                localStorage.setItem("id", loggedUser.id);
                localStorage.setItem("token", loggedUser.token);
                localStorage.setItem("firstName", loggedUser.firstName);
                localStorage.setItem("lastName", loggedUser.lastName);
                localStorage.setItem("email", loggedUser.email);
                history.push("/");
                // window.location.reload(true);
            }).catch((error) => {
            dispatch(authError(error));
        })
    }
}

export function register({Email, Password, FirstName, LastName}) {
    return function (dispatch) {
        axios.post(`${url}/user/auth/register`, {Email, Password, FirstName, LastName})
            .then(response => {
                const responseData = response.data;
                dispatch({type: REGISTER_USER, payload: responseData.message})
                history.push("/");
                alert(responseData.message);
            })
    }
}

export function getMessage() {
    return function (dispatch) {
        const token = localStorage.getItem("token");
        const config = {
            headers: {'Authorization': "bearer " + token}
        };

        axios.get(`${url}/user/auth/WelcomeMessage`, config).then(response => {
            const mess = response.data.message;
            dispatch({
                type: GET_MESSAGE,
                payload: mess
            })
        })

    }
}

export function getMainPageMessage() {
    return function (dispatch) {
        axios.get(`${url}/user/auth/mainpagemessage`).then(response => {
            const mess = response.data.message;
            dispatch({
                type: GET_HOME_PAGE_MESSAGE,
                payload: mess
            })
        })
    }

}

export function authError(error) {
    return {
        type: AUTH_ERROR,
        payload: error
    }
}