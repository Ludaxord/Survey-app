import React, {Component} from "react";
import "../Styles/App.css";
import "bootstrap/dist/css/bootstrap.css";
import {Provider} from "react-redux";
import {Router, Switch, Route} from "react-router";
import {applyMiddleware, compose, createStore} from "redux";
import promise from "redux-promise";
import reduxThunk from "redux-thunk";
import history from '../Helpers/History';
import Menu from "./Menu";
import MainPage from "./MainPage";
import Login from "./Login";
import Register from "./Register";
import Survey from "./Survey";
import SurveyList from "./SurveyList";
import Logout from "./Logout";
import Footer from "./Footer";
import reducers from "../Reducers"
import requireAuth from "../Helpers/requireAuth";
import {LOGIN_USER, UNAUTH_USER} from "../Types/types";
import noRequireAuth from "../Helpers/noRequireAuth";
import SurveyResults from "./SurveyResults";

const createStoreWithMiddleware = compose(applyMiddleware(promise, reduxThunk))(createStore);

const store = createStoreWithMiddleware(reducers);

const token = localStorage.getItem("token");

if (token) {
    store.dispatch({
        type: LOGIN_USER,
        token: token
    });
} else {
    store.dispatch({
        type: UNAUTH_USER
    })
}

class App extends Component {
    render() {
        return (
            <Provider store={store}>
                <div className="col-xs-12">
                    <Router history={history}>
                        <div>
                            <Menu/>
                            <div className="container">
                                <Switch>
                                    <Route exact path="/" component={MainPage}/>
                                    <Route path="/login" component={noRequireAuth(Login)}/>
                                    <Route path="/register" component={noRequireAuth(Register)}/>
                                    <Route path="/survey/:id" component={requireAuth(Survey)}/>
                                    <Route path="/Survey_list" component={requireAuth(SurveyList)}/>
                                    <Route path="/survey_result/:survey_id/:user_id" component={requireAuth(SurveyResults)}/>
                                    <Route path="/logout" component={requireAuth(Logout)}/>
                                </Switch>
                            </div>
                        </div>
                    </Router>
                    <Footer/>
                </div>
            </Provider>
        );
    }
}

export default App;
