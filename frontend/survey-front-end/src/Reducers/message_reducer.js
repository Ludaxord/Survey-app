import {GET_HOME_PAGE_MESSAGE, GET_MESSAGE} from '../Types/types';

export default function (state = {}, action) {
    switch (action.type) {
        case GET_MESSAGE:
            return {...state, welcome_message: action.payload};
        case GET_HOME_PAGE_MESSAGE:
            return {...state, home_page_message: action.payload};
    }
    return state;
}