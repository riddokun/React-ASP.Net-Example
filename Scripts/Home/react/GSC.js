import React from 'react';

class GSCRow extends React.Component {
    render() {  
        //const createDate = new Date(this.props.item.UploadedOn);
        
        return (
            <tr>
                <td>{this.props.item.ID}</td>
                <td>{this.props.item.FileName}</td>
                <td>{this.props.item.UploadedBy}</td>
                <td>{this.props.item.UploadedOn}</td>
                <td>{this.props.item.ModifiedBy}</td>
                <td>{this.props.item.ModifiedOn}</td>
                <td>
                    <a href="URL">EDIT</a> |
                    <a href="URL">VIEW</a>                    
                </td>
            </tr>
        );
    }
    
    //<button onClick={this.delete.bind(this, this.props.item.ID)}>Delete</button>

    //delete(item) {
    //    const data = this.state.item.filter(i => i.ID !== item.ID);
    //    this.setState({ data });
    //}
}

export default class GSCTable extends React.Component {
    // Initialize state right in the class body,
    // with a property initializer:
    state = {
        result: []
    }

    render() {
        var rows = [];
        this.state.result.forEach(function (item) {
            rows.push(<GSCRow key={item.ID} item={item} />);
        });
        return (
            <table className="table">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th>FileName</th>
                        <th>Uploaded By</th>
                        <th>Uploaded On</th>
                        <th>Created By</th>
                        <th>Created On</th>
                        <th>Action</th>
                    </tr>
                </thead>

                <tbody>
                    {rows}
                </tbody>
            </table>);
    }

    componentDidMount() {
        var xhr = new XMLHttpRequest();
        xhr.open('get', this.props.url, true);
        xhr.onload = function () {
            var response = JSON.parse(xhr.responseText);            
            this.setState({ result: response });
        }.bind(this);
        xhr.send();
    }

    delete(item) {
        const data = this.state.result.filter(i => i.id !== item.ID)
        this.setState({data})
    }
}
//ReactDOM.render(<EmployeeTable url="GetEmployeeData" />,
//    document.getElementById('griddata')
//);  