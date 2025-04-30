# API_Project

# 🐞 Bug Ticketing System

is a web-based application built with ASP.NET Core that helps software development teams efficiently track and manage bugs throughout the software lifecycle. The system supports multiple user roles — Managers, Developers, and Testers — each with specific permissions to report, assign, and resolve bugs.

## API Reference

---

### Register User

*POST* /api/Users/Register

| Body      | Type     | Description       |
|-----------|----------|-------------------|
| userName  | string   | Required.       |
| userRole  | string   | Required.       |
| password  | string   | Required.       |
| email     | string   | Required.       |

---

### Log In

*POST* /api/Users/Login

| Body      | Type     | Description       |
|-----------|----------|-------------------|
| userName  | string   | Required.       |
| password  | string   | Required.       |

---

### Get All Projects

*GET* /api/projects

Returns a list of all projects.

---

### Add New Project

*POST* /api/projects

| Body       | Type     | Description        |
|------------|----------|--------------------|
| name       | string   | Required.        |
| title      | string   | Required.        |

---

### View Project Details (and its Bugs)

*GET* /api/projects/{id}

| Parameter | Type   | Description        |
|-----------|--------|--------------------|
| id        | Guid   | Required.        |

---

### Report a New Bug

*POST* /api/bugs

| Body         | Type   | Description                     |
|--------------|--------|---------------------------------|
| description  | string | Required.                     |
| project_id   | Guid   | Required. Linked Project ID.  |

---

### List All Bugs

*GET* /api/bugs

Returns a list of all bugs.

---

### Get Bug by ID

*GET* /api/bugs/{id}

| Parameter | Type   | Description          |
|-----------|--------|----------------------|
| id        | Guid   | Required. Bug ID.  |

---

### Assign User to Bug

*POST* /api/bugs/{bug_id}/assign

| Parameter | Type   | Description           |
|-----------|--------|-----------------------|
| bug_id    | Guid   | Required. Bug ID.   |

| Body      | Type   | Description           |
|-----------|--------|-----------------------|
| user_id   | Guid   | Required. User ID.  |

---

### Unassign User from Bug

*DELETE* /api/bugs/{bug_id}/unassign/{user_id}

| Parameters  | Type   | Description            |
|-------------|--------|------------------------|
| bug_id      | Guid   | Required. Bug ID.    |
| user_id     | Guid   | Required. User ID.   |

---

### Upload Attachment to Bug

*POST* /api/bugs/{bug_id}/attachments

| Parameter | Type   | Description            |
|-----------|--------|------------------------|
| bug_id    | Guid   | Required. Bug ID.    |

| Body      | Type   | Description            |
|-----------|--------|------------------------|
| file      | File   | Required. File upload|

---

### Get Attachments for a Bug

*GET* /api/bugs/{bug_id}/attachments

| Parameter | Type   | Description            |
|-----------|--------|------------------------|
| bug_id    | Guid   | Required. Bug ID.    |

---

### Delete Attachment

*DELETE* /api/bugs/{bug_id}/attachments/{attachment_id}

| Parameters      | Type   | Description              |
|-----------------|--------|--------------------------|
| bug_id          | Guid   | Required. Bug ID.      |
| attachment_id   | Guid   | Required. Attachment ID|

---

## Models Overview

- *Project*
  - Id, Name, Title, ICollection<Bug> Bugs

- *Bug*
  - Id, Description, Project_id, Project, Attachments, User_Bugs# Project Title

Bug Ticketing System is a web-based application built with ASP.NET Core that helps software development teams efficiently track and manage bugs throughout the software lifecycle. The system supports multiple user roles — Managers, Developers, and Testers — each with specific permissions to report, assign, and resolve bugs.

## API Reference

---

### Register User

*POST* /api/Users/Register

| Body      | Type     | Description       |
|-----------|----------|-------------------|
| userName  | string   | Required.       |
| userRole  | string   | Required.       |
| password  | string   | Required.       |
| email     | string   | Required.       |

---

### Log In

*POST* /api/Users/Login

| Body      | Type     | Description       |
|-----------|----------|-------------------|
| userName  | string   | Required.       |
| password  | string   | Required.       |

---

### Get All Projects

*GET* /api/projects

Returns a list of all projects.

---

### Add New Project

*POST* /api/projects

| Body       | Type     | Description        |
|------------|----------|--------------------|
| name       | string   | Required.        |
| title      | string   | Required.        |

---

### View Project Details (and its Bugs)

*GET* /api/projects/{id}

| Parameter | Type   | Description        |
|-----------|--------|--------------------|
| id        | Guid   | Required.        |

---

### Report a New Bug

*POST* /api/bugs

| Body         | Type   | Description                     |
|--------------|--------|---------------------------------|
| description  | string | Required.                     |
| project_id   | Guid   | Required. Linked Project ID.  |

---

### List All Bugs

*GET* /api/bugs

Returns a list of all bugs.

---

### Get Bug by ID

*GET* /api/bugs/{id}

| Parameter | Type   | Description          |
|-----------|--------|----------------------|
| id        | Guid   | Required. Bug ID.  |

---

### Assign User to Bug

*POST* /api/bugs/{bug_id}/assign

| Parameter | Type   | Description           |
|-----------|--------|-----------------------|
| bug_id    | Guid   | Required. Bug ID.   |

| Body      | Type   | Description           |
|-----------|--------|-----------------------|
| user_id   | Guid   | Required. User ID.  |

---

### Unassign User from Bug

*DELETE* /api/bugs/{bug_id}/unassign/{user_id}

| Parameters  | Type   | Description            |
|-------------|--------|------------------------|
| bug_id      | Guid   | Required. Bug ID.    |
| user_id     | Guid   | Required. User ID.   |

---

### Upload Attachment to Bug

*POST* /api/bugs/{bug_id}/attachments

| Parameter | Type   | Description            |
|-----------|--------|------------------------|
| bug_id    | Guid   | Required. Bug ID.    |

| Body      | Type   | Description            |
|-----------|--------|------------------------|
| file      | File   | Required. File upload|

---

### Get Attachments for a Bug

*GET* /api/bugs/{bug_id}/attachments

| Parameter | Type   | Description            |
|-----------|--------|------------------------|
| bug_id    | Guid   | Required. Bug ID.    |

---

### Delete Attachment

*DELETE* /api/bugs/{bug_id}/attachments/{attachment_id}

| Parameters      | Type   | Description              |
|-----------------|--------|--------------------------|
| bug_id          | Guid   | Required. Bug ID.      |
| attachment_id   | Guid   | Required. Attachment ID|

---

## Models Overview

- *Project*
  - Id, Name, Title, ICollection<Bug> Bugs

- *Bug*
  - Id, Description, Project_id, Project, Attachments, User_Bugs
