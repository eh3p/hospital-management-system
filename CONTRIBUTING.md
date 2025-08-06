# Contributing to Hospital Management System API

Thank you for your interest in contributing to the Hospital Management System API! This document provides guidelines and information for contributors.

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022 or VS Code
- Git

### Development Setup
1. Fork the repository
2. Clone your fork: `git clone https://github.com/YOUR_USERNAME/hospital-management-system.git`
3. Navigate to the project: `cd hospital-management-system/HospitalManagementSystem`
4. Install dependencies: `dotnet restore`
5. Build the project: `dotnet build`
6. Run the application: `dotnet run`

## 📝 Code Style Guidelines

### C# Coding Standards
- Use PascalCase for class names, methods, and properties
- Use camelCase for local variables and parameters
- Use meaningful names for variables and methods
- Add XML documentation for public APIs
- Follow SOLID principles

### API Design Guidelines
- Use RESTful conventions
- Return appropriate HTTP status codes
- Include proper error handling
- Use DTOs for data transfer
- Implement proper validation

## 🔧 Development Workflow

### 1. Create a Feature Branch
```bash
git checkout -b feature/your-feature-name
```

### 2. Make Your Changes
- Write clean, readable code
- Add tests for new functionality
- Update documentation if needed

### 3. Commit Your Changes
```bash
git add .
git commit -m "feat: add new feature description"
```

### 4. Push and Create Pull Request
```bash
git push origin feature/your-feature-name
```

## 📋 Pull Request Guidelines

### Before Submitting
- [ ] Code follows the project's style guidelines
- [ ] All tests pass
- [ ] Documentation is updated
- [ ] No breaking changes (unless discussed)

### Pull Request Template
```markdown
## Description
Brief description of the changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Testing
- [ ] Unit tests added/updated
- [ ] Manual testing completed

## Checklist
- [ ] Code follows style guidelines
- [ ] Self-review completed
- [ ] Documentation updated
```

## 🐛 Reporting Issues

### Bug Reports
When reporting bugs, please include:
- Clear description of the issue
- Steps to reproduce
- Expected vs actual behavior
- Environment details (.NET version, OS, etc.)
- Screenshots if applicable

### Feature Requests
For feature requests, please include:
- Clear description of the feature
- Use cases and benefits
- Implementation suggestions if possible

## 📚 Documentation

### Code Documentation
- Add XML comments for public APIs
- Include examples in documentation
- Keep README.md updated

### API Documentation
- Update API_DOCUMENTATION.md for new endpoints
- Include request/response examples
- Document error codes

## 🧪 Testing

### Writing Tests
- Write unit tests for new functionality
- Use descriptive test names
- Follow AAA pattern (Arrange, Act, Assert)
- Mock external dependencies

### Running Tests
```bash
dotnet test
```

## 📞 Contact

For questions or support:
- **Author**: Ehab Ashraf Mourad
- **Email**: ehabashraf1667@gmail.com
- **GitHub**: [eh3p](https://github.com/eh3p)

## 📄 License

By contributing to this project, you agree that your contributions will be licensed under the MIT License.

---

Thank you for contributing to the Hospital Management System API! 🏥✨ 