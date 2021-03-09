import {BrowserRouter, Route, Switch} from "react-router-dom";
import Students from "./Pages/Students";

export default function Routes(){
    return (
        <BrowserRouter>
            <Switch>
                <Route path="/" exact>
                    <Students />
                </Route>
            </Switch>
        </BrowserRouter>
    )
}