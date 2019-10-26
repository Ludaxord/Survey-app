import {combineReducers} from "redux";
import {reducer as formReducer} from "redux-form";
import authReducer from "./auth_reducer";
import messageReducer from "./message_reducer";
import surveyReducer from "./survey_reducer";

const reducers = {
    form: formReducer,
    auth: authReducer,
    messages: messageReducer,
    surveys: surveyReducer,
};


const rootReducer = combineReducers(reducers);

export default rootReducer;