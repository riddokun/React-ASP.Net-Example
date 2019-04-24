import React from 'react';
import { LoginForm } from 'react-stormpath';

export default class LoginPage extends React.Component {
    render() {
        return (
            <table style="margin-left:auto; margin-right:auto;">
                <tr>
                    <td>
                        @Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.")
            
                        @using (Html.BeginForm((string)ViewBag.FormAction, "LogOn"))
                        {
                            <fieldset>
                                <legend>Log On Form</legend>
                                <table>
                                    <tr>
                                        <td>@Html.LabelFor(m => m.UserName)</td>
                                        <td>@Html.TextBoxFor(m => m.UserName, new {maxlength = 25, style = "width: 200px;" })</td>
                                        <td>@Html.ValidationMessageFor(m => m.UserName)</td>
                                    </tr>
                                    <tr>
                                        <td>@Html.LabelFor(m => m.Password)</td>
                                        <td>@Html.PasswordFor(m => m.Password, new {maxlength = 25, style = "width: 200px;" })</td>
                                        <td>@Html.ValidationMessageFor(m => m.Password)</td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            @Html.CheckBoxFor(m => m.RememberMe)
                                @Html.LabelFor(m => m.RememberMe, new { @class = "checkbox" })
                            </td>
                                        <td align="right" style="padding-bottom:25px">
                                            Need an account?
                                @Html.ActionLink("Sign up", "Register") <!--Insert Register actionLink here-->
                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center"><input type="submit" value="Log On" title="Log On" , style="text-align:center" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" valign="bottom">Forgot your Password? Please contact your HRG help desk.</td>
                                    </tr>
                                </table>
                            </fieldset>
                        }
                    </td>
                </tr>
            </table>
        );
    }
}