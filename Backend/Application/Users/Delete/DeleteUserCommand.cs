using Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Delete
{
    public record DeleteUserCommand(string Id):ICommand;
}
