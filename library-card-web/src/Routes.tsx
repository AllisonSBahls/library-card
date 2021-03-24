import {BrowserRouter, Route, Switch} from "react-router-dom";
import Students from "./Pages/Students";
import StudentForm from "./Pages/Students/StudentForm";

export default function Routes(){
    return (
        <BrowserRouter>
            <Switch>
                <Route path="/" exact>
                    <Students />
                </Route>
                <Route path="/estudante/registrar" exact>
                    <StudentForm />
                </Route>
            </Switch>
        </BrowserRouter>
    )
}