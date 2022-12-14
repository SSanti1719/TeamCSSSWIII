package com.teamcss.jobtime.ui.gallery;

import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.SpinnerAdapter;

import androidx.annotation.NonNull;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;

import com.teamcss.jobtime.MainActivity;
import com.teamcss.jobtime.R;
import com.teamcss.jobtime.databinding.FragmentGalleryBinding;
import com.teamcss.jobtime.model.Assign;
import com.teamcss.jobtime.model.ProjectManager;
import com.teamcss.jobtime.ui.dialog.DatePickerFragment;
import com.teamcss.jobtime.helpers.API;
import com.teamcss.jobtime.model.Employee;
import com.teamcss.jobtime.model.Project;
import com.teamcss.jobtime.ui.home.HomeFragment;

public class GalleryFragment extends Fragment implements View.OnClickListener {

    private FragmentGalleryBinding binding;

    private Button btnAsignar;
    private EditText etFechaEntr;

    public View onCreateView(@NonNull LayoutInflater inflater,
                             ViewGroup container, Bundle savedInstanceState) {
        GalleryViewModel galleryViewModel =
                new ViewModelProvider(this).get(GalleryViewModel.class);

        View view = inflater.inflate(R.layout.fragment_gallery, container, false);
        //etFechaEntr = (EditText) view.findViewById(R.id.etFechaEntr);
        //etFechaEntr.setOnClickListener(this);

        binding = FragmentGalleryBinding.inflate(inflater, container, false);
        View root = binding.getRoot();

        binding.spEmpleados.setAdapter((SpinnerAdapter) getEmployeeList());
        binding.spProyectos.setAdapter((SpinnerAdapter) getProjectList());
        binding.spAdminProyectos.setAdapter((SpinnerAdapter) getProjectManagerList());
        binding.btnAsignar.setOnClickListener(this);
        return root;
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        binding = null;
    }

    @Override
    public void onClick(View view) {
        System.out.println("View: " + view.getId());
        switch (view.getId()) {
            case R.id.btnAsignar:
                saveAssign();
                break;
        }
    }

    public ArrayAdapter<Employee> getEmployeeList() {
        ArrayAdapter<Employee> employees;
        try {
            API api = new API(getContext());
            employees = api.getEmployees();
        } catch (Exception ex) {
            //ex.printStackTrace();
            System.out.println("Excepción: " + ex.getMessage());
            return new ArrayAdapter<Employee>(getContext(), android.R.layout.simple_list_item_1);
        }

        //employees.add(new Employee("2466567", "Stiven Velez", "stiven@gmail.com"));
        //employees.add(new Employee("3575675", "Fabian Palacios", "stiven@gmail.com"));

        return employees;
    }

    public ArrayAdapter<Project> getProjectList() {
        ArrayAdapter<Project> projects;
        try {
            API api = new API(getContext());
            projects = api.getProjects();
        } catch (Exception ex) {
            //ex.printStackTrace();
            System.out.println("Excepción: " + ex.getMessage());
            return new ArrayAdapter<Project>(getContext(), android.R.layout.simple_list_item_1);
        }

        return projects;
    }

    public ArrayAdapter<ProjectManager> getProjectManagerList() {
        ArrayAdapter<ProjectManager> projectManagers;
        try {
            API api = new API(getContext());
            projectManagers = api.getProjectManagers();
        } catch (Exception ex) {
            //ex.printStackTrace();
            System.out.println("Excepción: " + ex.getMessage());
            return new ArrayAdapter<ProjectManager>(getContext(), android.R.layout.simple_list_item_1);
        }

        return projectManagers;
    }

    public void saveAssign() {
        AlertDialog.Builder builder1 = new AlertDialog.Builder(getContext());
        Employee employee = (Employee) binding.spEmpleados.getSelectedItem();
        Project project = (Project) binding.spProyectos.getSelectedItem();
        ProjectManager projectManager = (ProjectManager) binding.spAdminProyectos.getSelectedItem();
        //String hours = String.valueOf(binding.etHorasComp.getText());
        //String date = String.valueOf(binding.etFechaEntr.getText());

        try {
            API api = new API(getContext());
            if (api.saveAssign(new Assign(projectManager.getNit(), employee.getId(), project.getId()))) {
                builder1.setMessage("Proyecto asignado correctamente");
                Intent redirect=new Intent(getActivity(), MainActivity.class);
                setTimeout(() -> getActivity().startActivity(redirect), 000);
            } else {
                builder1.setMessage("Se presentó un problema al asignar el proyecto");
            }
            AlertDialog alert11 = builder1.create();
            alert11.show();
        } catch (Exception e) {
            builder1.setMessage("Se presentó un problema al asignar el proyecto");
            AlertDialog alert11 = builder1.create();
            alert11.show();
        }
    }

    public static void setTimeout(Runnable runnable, int delay){
        new Thread(() -> {
            try {
                Thread.sleep(delay);
                runnable.run();
            }
            catch (Exception e){
                System.err.println(e);
            }
        }).start();
    }

    private void showDatePickerDialog() {
        DatePickerFragment newFragment = DatePickerFragment.newInstance(new DatePickerDialog.OnDateSetListener() {
            @Override
            public void onDateSet(DatePicker datePicker, int year, int month, int day) {
                // +1 because January is zero
                final String selectedDate = day + " / " + (month+1) + " / " + year;
                //binding.etFechaEntr.setText(selectedDate);
            }
        });

        newFragment.show(getActivity().getSupportFragmentManager(), "datePicker");
    }
}