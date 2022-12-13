package com.teamcss.jobtime.model;

public class Assign {
    private String id;
    private String nitProjectmanager;
    private String nitEmployee;
    private String nitProject;

    public Assign(String id, String nitProjectmanager, String nitEmployee, String nitProject) {
        this.id = id;
        this.nitProjectmanager = nitProjectmanager;
        this.nitEmployee = nitEmployee;
        this.nitProject = nitProject;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getNitProjectmanager() {
        return nitProjectmanager;
    }

    public void setNitProjectmanager(String nitProjectmanager) {
        this.nitProjectmanager = nitProjectmanager;
    }

    public String getNitEmployee() {
        return nitEmployee;
    }

    public void setNitEmployee(String nitEmployee) {
        this.nitEmployee = nitEmployee;
    }

    public String getNitProject() {
        return nitProject;
    }

    public void setNitProject(String nitProject) {
        this.nitProject = nitProject;
    }
}
