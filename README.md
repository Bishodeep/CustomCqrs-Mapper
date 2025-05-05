
# 🔧 CqrsMapLibrary - Custom CQRS & Mapper Library for .NET

## ✨ Overview

**CqrsMapLibrary** is a lightweight, flexible replacement for **MediatR** and **AutoMapper**.  
Built for clean architecture and CQRS-based solutions in .NET, it's designed with **performance**, **simplicity**, and **extensibility** in mind.

✅ Built from scratch — no third-party runtime dependencies  
✅ Clean separation of **Commands**, **Queries**, and **Handlers**  
✅ Integrated object mapper with zero reflection and high speed  
✅ Supports .NET 8


## 🚀 Getting Started

### 🧱 Register the services

```csharp
builder.Services.AddCqrs();
```

---

### 📤 Define a Command

```csharp
public record CreateUserCommand(string Name, string Email) : ICommand<UserDto>;
```

### 🛠️ Implement the Command Handler

```csharp
public class CreateUserHandler : ICommandHandler<CreateUserCommand, UserDto>
{
    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User { Name = request.Name, Email = request.Email };
        return new UserDto(user.Name, user.Email);
    }
}
```

---

### 🔄 Use the Mapper

```csharp
var userDto = _mapper.Map<User, UserDto>(user);
```

---

## 🧪 Unit Testing

All handlers are built with testability in mind. You can easily test command/query handlers with mock dependencies and no runtime injection requirements.

---
## 🤝 Contributing

Contributions, suggestions, and feedback are welcome!  
Just fork the repo, create a branch, and submit a pull request.

