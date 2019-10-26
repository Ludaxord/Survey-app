import {AUTH_ERROR, LOGIN_USER, REGISTER_USER, UNAUTH_USER} from '../Types/types';

export default function (state = {}, action) {
    switch (action.type) {
        case LOGIN_USER:
            return {...state, authenticated: true, token: action.token};
        case UNAUTH_USER:
            return {...state, authenticated: false, token: null};
        case AUTH_ERROR:
            return {...state, error: action.payload};
        case REGISTER_USER:
            return {...state, register_message: action.payload}
    }
    return state;
}