import React, {Component, Fragment} from 'react';
import { render } from 'react-dom';
import ReactDOM from 'react-dom';
import {
    BrowserRouter as Router,
    Route,
    Link
} from 'react-router-dom';

import GSC from './GSC';
import Intro from './Intro';
import EmployeeTable from './GSC';
//<Route exact path='/Home/GSC' component={() => <GSC url="/Home/GetEmployeeData" />} />
class App extends Component {
    render() {
        return (
            <Router>
            <div className="App">

                <Route exact path='/' component={Intro} />
                <Route exact path='/Home/GSC' render={() => <EmployeeTable url="GetGSCData" />} />
    
            </div>
            </Router>
        );
    }
}

ReactDOM.render(<App />, document.getElementById('app'));