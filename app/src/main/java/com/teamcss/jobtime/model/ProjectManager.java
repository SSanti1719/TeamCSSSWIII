package com.teamcss.jobtime.model;

public class ProjectManager {
    private String nit;
    private String name;
    private String email;
    private String jobTitle;

    public ProjectManager(String nit, String name, String email, String jobTitle) {
        this.nit = nit;
        this.name = name;
        this.email = email;
        this.jobTitle = jobTitle;
    }

    public String getNit() {
        return nit;
    }

    public void setNit(String nit) {
        this.nit = nit;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getJobTitle() {
        return jobTitle;
    }

    public void setJobTitle(String jobTitle) {
        this.jobTitle = jobTitle;
    }

    @Override
    public String toString() {
        return this.name;
    }
}
